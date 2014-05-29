using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JUITool;

namespace JUITool
{
		public class ConnectableWnd : BaseWindow,IConnectableWnd
		{
		private string mLinkBtn = "Link";

		public string LinkBtn{

			get{
				return mLinkBtn;
			}
			set{}
		}
				
				private string mDetachkBtn = "Break";
				private Rect mLinkRect = new Rect (5, 45, 45, 20);
				private Rect mDetachRect = new Rect (50, 45, 45, 20);
			
				public ConnectableWnd (string Name, EventEditorWindow win):base(Name, win)
				{
						//may consider property getter, setter
						if (GetWndProp () != WindowProperty.none)
								SetWndProp (WindowProperty.connectable);

					OnRemoveEvent += new OnRemoveWindowDelegate(RemoveConnectedWnd);
			OnLinkEvent += new OnLinkDelegate (linkAction);
			OnDelinkEvent += new OnDelinkDelegate (delinkAction);

				}

				public override void OnGUI (int i)
				{
						if (GUI.Button (mLinkRect, mLinkBtn)) {
								PairingWndProc (this, PairingProc.link);
								
						} else if (GUI.Button (mDetachRect, mDetachkBtn)) {
								PairingWndProc (this, PairingProc.detach);
						}
						base.OnGUI (i);
				}
	
				//Welcome to devise better algorithm, maybe using delegates or etc.
				public List<ConnectableWnd> mConnectedWnd = new List<ConnectableWnd> ();
				public static ConnectableWnd mPairWnd1;
				private static ConnectableWnd mPairWnd2; 

				private enum PairingProc
				{
						none,
						link,
						detach
				}
				static PairingProc proc = PairingProc.none;

				private void PairingWndProc (ConnectableWnd aWnd, PairingProc processing)
				{
						if (mPairWnd1 == null && mPairWnd2 == null) {
								if (proc == PairingProc.none) {
										proc = processing;
										mPairWnd1 = aWnd;
								} else
										Debug.Log ("unknown situation");
						} else if ((mPairWnd1 != null && mPairWnd2 == null) || (mPairWnd1 == null && mPairWnd2 != null)) {
								if (proc == PairingProc.none) 
										Debug.Log ("strange case");
								else if (proc != processing) {
										resetMeta ();
								} else {
										mPairWnd2 = aWnd;
		
										if (mPairWnd1 == mPairWnd2) {
												resetMeta ();
										} else {
												switch (proc) {
												case PairingProc.link:
							OnLinkEvent();
														//AttachWindow (mPairWnd1, mPairWnd2);
														resetMeta ();
														break;
												case PairingProc.detach:
							OnDelinkEvent();
														//DetachWindow (mPairWnd1, mPairWnd2);
														resetMeta ();
														break;
												default:
														Debug.Log ("fuck you");
														break;
												}
										}
								}
						}
				}

		public delegate void OnLinkDelegate ();
		public delegate void OnDelinkDelegate ();
		public event OnLinkDelegate OnLinkEvent;
		public event OnDelinkDelegate OnDelinkEvent;

		public void linkAction() {
			//actually only win2's AttactWindow() and win2's DetachWindow() called, you may suppose to start from this=win2 case
			if (!mPairWnd2.mConnectedWnd.Contains (mPairWnd1))
				mPairWnd2.mConnectedWnd.Add (mPairWnd1);
			if (!mPairWnd1.mConnectedWnd.Contains (mPairWnd2))
				mPairWnd1.mConnectedWnd.Add (mPairWnd2);
				}

		public void delinkAction(){
			//since win2 always is the last clicked target wnd
			if (mPairWnd2.mConnectedWnd.Contains (mPairWnd1)) {
				if (mPairWnd1.mConnectedWnd.Contains (mPairWnd2)) {
					mPairWnd1.mConnectedWnd.Remove (mPairWnd2);
				}
				mPairWnd2.mConnectedWnd.Remove (mPairWnd1);
			}
				}

				public static void resetMeta ()
				{
						//reset all
						mPairWnd1 = null;
						mPairWnd2 = null;
						proc = PairingProc.none;
				}

		//========================================================================
		void RemoveConnectedWnd (BaseWindow rmWnd) {

			foreach (ConnectableWnd frd in mConnectedWnd)
				frd.mConnectedWnd.Remove ((ConnectableWnd)this);

			if (ConnectableWnd.mPairWnd1 == this) {
				ConnectableWnd.resetMeta ();
			}
		}
		//========================================================================

		}
	

	public class WndContainer {

		private List<BaseWindow> mList = new List<BaseWindow>();

		//should create a generic type of combination container for mNodeCurvePairs
		//i.e. exclude same combination elements
		public struct ConnectPair
		{
			//no good for abstraction
			public ConnectableWnd wndA;
			public ConnectableWnd wndB;
			
			public ConnectPair (ConnectableWnd aWndA, ConnectableWnd aWndB)
			{
				wndA = aWndA;
				wndB = aWndB;
			}
			
			public ConnectPair GetReverse ()
			{
				return new ConnectPair (wndB, wndA);
			}
			
			//better consider an kind of acessor to automatically refresh, since it is linked to member: mOnScreenWindows
		}

		List<ConnectPair> mNodeCurvePairs = new List<ConnectPair> ();

		public List<BaseWindow> GetWnds () {
						return mList;
				}

		public List<ConnectPair> GetConnectPair() {
						return mNodeCurvePairs;
				}

		public void Add( BaseWindow aWnd){
			mList.Add(aWnd);
			aWnd.OnRemoveEvent += new BaseWindow.OnRemoveWindowDelegate (Remove);

			if (aWnd.GetType () == typeof(JUITool.ConnectableWnd)) {
				ConnectableWnd temp = (ConnectableWnd)aWnd;
				temp.OnLinkEvent += GetNodeCurvePairs;
				temp.OnDelinkEvent += RegetNodeCurvePairs;
						}
		}

		public void Remove (BaseWindow aWnd) {

			if (mList.Contains (aWnd)){
				mList.Remove(aWnd);	
			} else
				Debug.Log ("Error, mList doesnt contain this base window!!");

			if (aWnd.GetType () == typeof(JUITool.ConnectableWnd))
				RegetNodeCurvePairs();
		}

		public void GetNodeCurvePairs ()
		{
			//really awful for this temporary function
			foreach (ConnectableWnd aWindow in mList) {
				foreach (ConnectableWnd aFrd in aWindow.mConnectedWnd) {
					ConnectPair temp = new ConnectPair (aWindow, aFrd);
					ConnectPair temp2 = temp.GetReverse ();
					if (!mNodeCurvePairs.Contains (temp) && !mNodeCurvePairs.Contains (temp2)) {
						mNodeCurvePairs.Add (temp);
					}
				}
			}
		}

		void RegetNodeCurvePairs ()
		{
			mNodeCurvePairs.Clear ();
			GetNodeCurvePairs ();
		}
	}
}
