using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxMenu
{
    /// <summary>
    /// A button whose four states are described by four separate Texture2Ds.
    /// </summary>
    public class ImageButton : BoxButton
    {
        private readonly Texture2D inactiveTexture, activeTexture, hoverTexture, clickTexture;
        private readonly Color activeColor, inactiveColor, hoverColor, clickColor;

        private Texture2D currentTexture;
        private Color currentColor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boundingBox">The bounding box of the button.</param>
        /// <param name="inactiveTexture">The texture when the button is disabled (but visible!).</param>
        /// <param name="activeTexture">The texture when the button is enabled.</param>
        /// <param name="hoverTexture">The texture when the mouse is hovering over the button.</param>
        /// <param name="clickTexture">The texture when the mouse is clicking on the button.</param>
        /// <param name="actionFunction">The delegate to the function raised by the button.</param>
        /// <param name="arguments">Any arguments to pass to the function raised by the button.</param>
        public ImageButton(Rectangle boundingBox,
            ActionDelegate actionFunction, object[] arguments,

            Texture2D inactiveTexture,
            Texture2D activeTexture,
            Texture2D hoverTexture,
            Texture2D clickTexture,

            Color? inactiveColor = null,
            Color? activeColor = null,
            Color? hoverColor = null,
            Color? clickColor = null
            )
            : base(boundingBox, actionFunction, arguments)
        {
            this.inactiveTexture = inactiveTexture;
            this.activeTexture = activeTexture;
            this.hoverTexture = hoverTexture;
            this.clickTexture = clickTexture;

            this.inactiveColor = inactiveColor ?? Color.White;
            this.activeColor = activeColor ?? Color.White;
            this.hoverColor = hoverColor ?? Color.White;
            this.clickColor = clickColor ?? Color.White;

            currentTexture = activeTexture;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            spriteBatch.Draw(currentTexture,
                BoundingBox,
                null,
                currentColor);
        }

        internal override void UpdateAppearance()
        {
            switch (state)
            {
                case BoxButtonState.Active:
                    currentTexture = activeTexture;
                    currentColor = activeColor;
                    break;
                case BoxButtonState.Inactive:
                    currentTexture = inactiveTexture;
                    currentColor = inactiveColor;
                    break;
                case BoxButtonState.Hovering:
                    currentTexture = hoverTexture;
                    currentColor = hoverColor;
                    break;
                case BoxButtonState.Clicking:
                    currentTexture = clickTexture;
                    currentColor = clickColor;
                    break;
            }
        }
    }
}
