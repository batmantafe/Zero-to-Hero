using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookieClicker
{
    public class Cursor : Building
    {
        protected override void OnBuy()
        {
            CursorManager.Instance.AddCursor();
        }
    }
}
