using System;

namespace DC.Template {

    [Serializable]
    public class MinionTM {
        public int templateID;
        public string name;
        public string modName;
        public int hp;
        public int atk;
        public int def;

        public int width;
        public int height;

        public MinionTM() { }

    }

}