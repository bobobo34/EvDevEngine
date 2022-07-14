using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EvDevEngine.EvDevEngine
{
    public class Animation2D : Component
    {
        public List<Texture2D> Textures;
        public Texture2D Spritesheet;
        public Texture2D CurrentSprite;
        public Func<bool> condition;
        public bool Animating = false;
        public int BeginAnimFrame;
        Vector2 PreviousScale;
        float floatPreviousScale = 1f;
        private int width;
        private int height;
        private float ScaleMultiplier = 1f;
        public bool constant = false;
        Texture2D previousTexture;
        Rectangle? previousRectangle;
        float Scale = 1f;
        public int TextureNum
        {
            get
            {
                return Textures.Count();
            }
        }
        public int FrameTimer;
        public Animation2D(Texture2D spritesheet, int individualWidth, int individualHeight, int frameTimer, Func<bool> condition, bool constant = false)
        {
            
            this.constant = constant;
            width = individualWidth;
            height = individualHeight;
            Textures = XNAfuncs.SpliceSpriteSheet(spritesheet, individualWidth, individualHeight);
            this.Spritesheet = spritesheet;
            FrameTimer = frameTimer;
            this.condition = condition;
        }
        public Animation2D(Texture2D spritesheet, int individualWidth, int individualHeight, int frameTimer, Func<bool> condition, float Scale, bool constant = false)
        {

            this.constant = constant;
            width = individualWidth;
            height = individualHeight;
            Textures = XNAfuncs.SpliceSpriteSheet(spritesheet, individualWidth, individualHeight);
            this.Spritesheet = spritesheet;
            FrameTimer = frameTimer;
            this.condition = condition;
            this.Scale = Scale;
        }

        int index = 0;
        
        public void Next()
        {
            index++;
            if (index == Textures.Count)
            {
                if(this.constant == false)
                {
                    EndAnimation();
                    return;
                }
                else
                {
                    index = 0;
                }
            }
            CurrentSprite = Textures[index];
            Parent.Sprite.Sprite = CurrentSprite;
        }
        public void StepAnimation(int Updates)
        {
            if (!condition()) EndAnimation();
            
            if ((Updates - BeginAnimFrame) % FrameTimer == 0) Next();
            Parent.Sprite.DrawSelf();
            //sprites.Draw(texture: Spritesheet, destinationRectangle: Parent.Sprite.rectangle, sourceRectangle: CurrentSprite, color: Color.White, rotation: 0f, effects: Parent.Sprite.Flipped, origin: XNAfuncs.Vec2(Vector2.Zero()), layerDepth: 0f);
        }
        public void EndAnimation()
        {
            Animating = false;
            if (Parent.Sprite.FromRectangle == true)
                Parent.Sprite.Scale = new Vector2(PreviousScale);
            else Parent.Sprite.floatScale = floatPreviousScale;
            
            Parent.Sprite.Sprite = previousTexture;
            Parent.Sprite.SourceRectangle = previousRectangle;
        }
        public void BeginAnimation(int Updates)
        {
            //if (!Parent.Sprite.DataReceived) return;
            if (Parent.Sprite.FromRectangle == true)
            {
                this.ScaleMultiplier = (float)Parent.Sprite.Scale.X / Parent.Sprite.Sprite.Width;
                PreviousScale = new Vector2(Parent.Sprite.Scale);
                Parent.Sprite.Scale *= Scale;
            }
            else
            {
                this.ScaleMultiplier = Parent.Sprite.floatScale;
                floatPreviousScale = Parent.Sprite.floatScale;
                Parent.Sprite.floatScale *= Scale;
            }
            
         
            CurrentSprite = Textures[0];
                                
            index = 0;
            BeginAnimFrame = Updates;
            Animating = true;

            previousTexture = Parent.Sprite.Sprite;
            previousRectangle = Parent.Sprite.SourceRectangle;

            Parent.Sprite.Sprite = Spritesheet;
            Parent.Sprite.Sprite = CurrentSprite;
        }
    }
}
