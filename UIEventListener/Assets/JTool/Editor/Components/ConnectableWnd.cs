using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JUITool;

namespace JUITool
{
		public class ConnectableWnd : BaseWindow,IConnectableWnd
		{
		private string mLinkBtn = "Link";

		public string linkBtn{

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
						//here to specify the size of rect and btn name
						Rect initWndSize = new Rect (30, 250, 100, 100);
						mWndRect = initWndSize;
				}

				public override void OnGUI (int i)
				{
						if (GUI.Button (mLinkRect, mLinkBtn)) {
								//mEditWndHdr.AttachWindow (this);
								PairingWndProc (this, PairingProc.link);
								
						} else if (GUI.Button (mDetachRect, mDetachkBtn)) {
								//mEditWndHdr.DettachWindow (this);
								PairingWndProc (this, PairingProc.detach);
						}
						base.OnGUI (i);
				}

				//unknown for use, but guessing should put in connectableWnd class
				/*public void Attaching() //it should not be declared in the class, if suppose not all windows are connectable
	{
		Color origin = GUI.color;
		GUI.color = Color.red;
		GUI.Box(new Rect(mWndRect.x, mWndRect.y, 100,15), "");
		GUI.color = origin;
	}*/ 

		#region AI handling clicking link and break btn
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
														AttachWindow (mPairWnd1, mPairWnd2);
														resetMeta ();
														break;
												case PairingProc.detach:
														DetachWindow (mPairWnd1, mPairWnd2);
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

				void AttachWindow (ConnectableWnd win1, ConnectableWnd win2)
				{
						//actually only win2's AttactWindow() and win2's DetachWindow() called, you may suppose to start from this=win2 case
						if (!win2.mConnectedWnd.Contains (win1))
								win2.mConnectedWnd.Add (win1);
						if (!win1.mConnectedWnd.Contains (win2))
								win1.mConnectedWnd.Add (win2);
				}
		
				void DetachWindow (ConnectableWnd win1, ConnectableWnd win2)
				{
						//since win2 always is the last clicked target wnd
						//Debug.Log ("connectable wnd: DetachWindow() called");
						//Debug.Log ("now win1 is : " + win1.mWndTitleName + " and win2 is: " + win2.mWndTitleName + " and this is: " + this.mWndTitleName);
						if (win2.mConnectedWnd.Contains (win1)) {
								if (win1.mConnectedWnd.Contains (win2)) {
										win1.mConnectedWnd.Remove (win2);
								}
								win2.mConnectedWnd.Remove (win1);
						}
						//mEditWndHdr.ClearNodeCurvePairs ();
				}

		delegate void OnLinkDelegate ();

		event OnLinkDelegate OnLinkEvent;

		public void linkAction() {
				}

		public void delinkAction(){
				}

				public static void resetMeta ()
				{
						//reset all
						mPairWnd1 = null;
						mPairWnd2 = null;
						proc = PairingProc.none;
				}
		#endregion
				//may consider put the connectable window interface here as well
		}
}
