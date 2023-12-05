using System.Collections.Generic;

namespace GreenRedNamespace {

    public class GreenRedRepo {

        GreenRedEntity current;
        public GreenRedEntity Current => current;

        public GreenRedRepo() {}

        public void SetCurrent(GreenRedEntity current) {
            this.current = current;
        }

    }

}