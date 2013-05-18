using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Solar.Graphics.Cameras
{
    public class BasicCamera2D
    {
        protected float _zoom;
        public Matrix _transform;
        public Vector2 _pos, _posModified;
        protected float _rotation;
        int previousMouseScrollWheelValue = 0;
        float zoomAmount = 1f;

        public BasicCamera2D()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            _posModified = _pos;
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; if (_zoom > 1.0f) _zoom = 1.0f; }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

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
                zoomAmount = Zoom + (mouseScrollWheelDifference / 2000f);
            }
            Zoom += (SmoothTransition(Zoom, zoomAmount, 0.1f));
            previousMouseScrollWheelValue = mouseState.ScrollWheelValue;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =
                Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }

        private float SmoothTransition(float pos1, float pos2, float speed)
        {

            float smoothedPosUpdate = (pos2 - pos1) * speed;
            if (smoothedPosUpdate < -0.1f) smoothedPosUpdate = -0.1f;
            return smoothedPosUpdate;
        }

        private Vector2 SmoothTransition(Vector2 pos1, Vector2 pos2, float speed)
        {

            Vector2 smoothedPosUpdate = (pos2 - pos1) * new Vector2 (speed, speed);
            return smoothedPosUpdate;
        }
    }
}
