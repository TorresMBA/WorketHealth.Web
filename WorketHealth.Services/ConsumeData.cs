using WorketHealth.DataAccess;

namespace WorketHealth.Services {
    public class ConsumeData {

        public int Consumiendo()
        {
            ClaseTest claseTest = new ClaseTest();

            var valorDevuelto = claseTest.Devolver();

            return valorDevuelto;
        }
    }
}