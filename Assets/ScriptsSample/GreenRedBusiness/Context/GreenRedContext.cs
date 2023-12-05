namespace GreenRedNamespace
{

    public class GreenRedContext
    {

        GreenRedRepo greenRedRepo;
        public GreenRedRepo GreenRedRepo => greenRedRepo;

        public GreenRedContext() { 
            this.greenRedRepo = new GreenRedRepo();
        }

    }

}