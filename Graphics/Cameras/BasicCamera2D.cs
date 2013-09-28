using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Solar.Graphics.Cameras
{
    public class BasicCamera2D
    {
        public Vector2 _pos, _posModified;
        protected float _rotation;
        public Matrix _transform;
        protected float _zoom;
        private int previousMouseScrollWheelValue;
        private float zoomAmount = 1f;

        /// <summary>
        ///     Constuctor which initializes variables.
        /// </summary>
        public BasicCamera2D()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            _posModified = _pos;
        }

        /// <summary>
        ///     Gets and sets the zoom variable.
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.16f) _zoom = 0.16f;
                if (_zoom > 1.0f) _zoom = 1.0f;
            }
        }

        /// <summary>
        ///     Gets and sets the rotation variable.
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        /// <summary>
        ///     Increments the modified position by an amount.
        /// </summary>
        /// <param name="amount">Amount to increment the modified position.</param>
        public void Move(Vector2 amount)
        {
            _posModified += amount;
        }

        public void Update(MouseState mouseState)
        {
            _pos += SmoothTransition(_pos, _posModified, 0.1f);

            float mouseScrollWheelDifference = mouseState.ScrollWheelValue - previousMouseScrollWheelValue;
            if (mouseScrollWheelDifference != 0)
            {
                zoomAmount = Zoom + (mouseScrollWheelDifference/2000f);
            }
            Zoom += (SmoothTransition(Zoom, zoomAmount, 0.1f));
            previousMouseScrollWheelValue = mouseState.ScrollWheelValue;
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =
                Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0))*
                Matrix.CreateRotationZ(Rotation)*
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))*
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width*0.5f,
                    graphicsDevice.Viewport.Height*0.5f, 0));
            return _transform;
        }

        /// <summary>
        ///     Smoothes the transition from one float to the other.
        /// </summary>
        /// <param name="pos1">Float to transition from.</param>
        /// <param name="pos2">Float to transition to.</param>
        /// <param name="speed">Speed of which the transition occurs.</param>
        /// <returns></returns>
        private float SmoothTransition(float pos1, float pos2, float speed)
        {
            float smoothedPosUpdate = (pos2 - pos1)*speed;
            if (smoothedPosUpdate < -0.1f) smoothedPosUpdate = -0.1f;
            return smoothedPosUpdate;
        }

        /// <summary>
        ///     Smoothes the transition from one Vector2 to the other.
        /// </summary>
        /// <param name="pos1">Vector2 to transition from.</param>
        /// <param name="pos2">Vector2 to transition to.</param>
        /// <param name="speed">Speed of which the transition occurs.</param>
        /// <returns></returns>
        private Vector2 SmoothTransition(Vector2 pos1, Vector2 pos2, float speed)
        {
            Vector2 smoothedPosUpdate = (pos2 - pos1)*new Vector2(speed, speed);
            return smoothedPosUpdate;
        }
    }
}