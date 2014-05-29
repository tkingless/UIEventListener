using UnityEngine;
using System.Collections;
using JUITool;

namespace JUITool
{
		public interface IConnectableWnd
		{

				string LinkBtn { get; set; }

				//string delinkBtn { get; set; }

				void linkAction ();

				void delinkAction ();

		}
}
