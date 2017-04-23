using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxMenu
{
    /// <summary>
    /// A collection of buttons (i.e. a "screen").
    /// </summary>
    public class ButtonCollection
    {
        private List<BoxButton> buttons = new List<BoxButton>();
        private MouseState nowState, prevState;

        /// <summary>
        /// Offset of button from its original position.
        /// </summary>
        public Point Offset;

        internal int TIMER_MAX = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="click_timer">How long the button should stay pressed after click.</param>
        public ButtonCollection(int click_timer)
        {
            TIMER_MAX = click_timer;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ButtonCollection() { }

        /// <summary>
        /// Gets the number of buttons in this collection.
        /// </summary>
        public int Count
        {
            get { return buttons.Count; }
        }

        /// <summary>
        /// Add a button to this collection.
        /// </summary>
        /// <param name="button"></param>
        public void Add(BoxButton button)
        {
            buttons.Add(button);
            button.TIMER_MAX = TIMER_MAX;
        }

        /// <summary>
        /// Remove a button from the collection.
        /// </summary>
        /// <param name="button"></param>
        public void Remove(BoxButton button)
        {
            buttons.Remove(button);
        }

        /// <summary>
        /// Remove a button at the given index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            buttons.RemoveAt(index);
        }

        /// <summary>
        /// Removes all buttons from the collection.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
        }

        /// <summary>
        /// Update all the buttons in this button collection.
        /// </summary>
        public void Update()
        {
            prevState = nowState;
            nowState = Mouse.GetState();

            bool blocked = false;

            // Go in reverse order, so upper buttons block lower buttons.
            for (int i = buttons.Count-1; i >= 0; i--)
            {
                if (buttons[i].Update(nowState, blocked, Offset))
                    blocked = true;
            }
        }

        /// <summary>
        /// Draw all the buttons in this collection.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Draw(spriteBatch, Offset);
        }

        /// <summary>
        /// Set the Enabled property of every button in the collection.
        /// </summary>
        /// <param name="enabled"></param>
        private void SetEnabledAll(bool enabled)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Enabled = enabled;
            }
        }

        /// <summary>
        /// Disable every button in the collection.
        /// </summary>
        public void DisableAll()
        {
            SetEnabledAll(false);
        }

        /// <summary>
        /// Enable every button in the collection.
        /// </summary>
        public void EnableAll()
        {
            SetEnabledAll(true);
        }

        /// <summary>
        /// Foreach construct.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<BoxButton> GetEnumerator()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                yield return buttons[i];
            }
        }

        /// <summary>
        /// Gets the button at this index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BoxButton this[int index]
        {
            get { return buttons[index]; }
        }
    }
}
