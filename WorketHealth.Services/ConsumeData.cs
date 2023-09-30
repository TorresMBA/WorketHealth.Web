using WorketHealth.DataAccess;

namespace WorketHealth.Services {
    public class ConsumeData {

        public int Consumiendo()
        {
            WorketHealthContext claseTest = new WorketHealthContext();

            var valorDevuelto = claseTest.Devolver();

            return valorDevuelto;
        }
    }
}