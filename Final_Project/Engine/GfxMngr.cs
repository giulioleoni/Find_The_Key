using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;
using Aiv.Fast2D;

namespace Final_Project
{
    enum Animations { Walk, Idle, LAST}

    static class GfxMngr
    {
        private static Dictionary<string, Texture> textures;
        private static Dictionary<string, AudioClip> clips;

        static GfxMngr()
        {
            textures = new Dictionary<string, Texture>();
            clips = new Dictionary<string, AudioClip>();
        }


        public static Texture AddTexture(string name, string path)
        {
            Texture t = new Texture(path);

            if(t != null)
            {
                textures[name] = t;
            }

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;

            if(textures.ContainsKey(name))
            {
                t = textures[name];
            }

            return t;
        }

        public static AudioClip AddClip(string name, string path)
        {
            AudioClip c = new AudioClip(path);

            if (c != null)
            {
                clips[name] = c;
            }

            return c;
        }

        public static AudioClip GetClip(string name)
        {
            AudioClip c = null;

            if (clips.ContainsKey(name))
            {
                c = clips[name];
            }

            return c;
        }

        public static void ClearAll()
        {
            textures.Clear();
            clips.Clear();
        }

    }
}
