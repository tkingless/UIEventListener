using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JUITool;

namespace JUITool
{
		public class ConnectableWnd : BaseWindow,IConnectableWnd
		{

				private string mLinkBtn = "Link";
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
						if (win1 == this) {
								if (!this.mConnectedWnd.Contains (win2))
										this.mConnectedWnd.Add (win2);
						} else if (win2 == this) {
								if (!this.mConnectedWnd.Contains (win1))
										this.mConnectedWnd.Add (win1);
						}

				}
		
				void DetachWindow (ConnectableWnd win1, ConnectableWnd win2)
				{
						if (win1 == this) {
								if (this.mConnectedWnd.Contains (win2))
										this.mConnectedWnd.Remove (win2);
						} else if (win2 == this) {
								if (this.mConnectedWnd.Contains (win1))
										this.mConnectedWnd.Remove (win1);
						}	
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
