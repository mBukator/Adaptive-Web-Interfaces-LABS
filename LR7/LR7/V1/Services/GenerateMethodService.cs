namespace LR7.V1.Services {
    public class GenerateMethodService : IGenerateMethodService {

        public int GenerateRandomNumber() {
            Random rand = new Random();
            return rand.Next(1, 999);
        }
    }
}
