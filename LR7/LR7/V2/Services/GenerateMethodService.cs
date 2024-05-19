namespace LR7.V2.Services {
    public class GenerateMethodService : IGenerateMethodService {

        public string GenerateRandomString() {
            Random random = new Random();
            String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            int size = 20;
            String randStr = "";

            for (int i = 0; i< size; i++) {
                int x = random.Next(70);    //70 because in string variable "str" 70 characters so max range is going to be like that

                randStr += str[x];
            }

            return $"Random string: {randStr}";
        }
    }
}
