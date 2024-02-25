namespace LR4.Services {
    internal class Games {
        private int _id;
        public string name;
        internal char gameClassification;
        private protected double percentOfPositiveReviews;
        protected bool isTopSellerOnSteam;


        //  Class constructor
        public Games(int _id, string name, char gameClassification, double percentOfPositiveReviews, bool isTopSellerOnSteam) { 
            this._id = _id;
            this.name = name;
            this.gameClassification = gameClassification;
            this.percentOfPositiveReviews = percentOfPositiveReviews;
            this.isTopSellerOnSteam = isTopSellerOnSteam;
        }


        // Class methods
        public void GetFullInfo() {
            string sellingInfo = isTopSellerOnSteam ? "Top seller" : "Not top seller";
            Console.WriteLine("====[ " + name + " ]====\n"
                              + "[+] Positive reviews: " + percentOfPositiveReviews + "%\n"
                              + "[+] Game Classification: " + gameClassification + "\n"
                              + "[+] Game is: " + sellingInfo + "\n");
        }

        internal protected void GetGameId(Games game) {
            Console.WriteLine("ID of " + game.name + " is: " + game._id);
        }

        protected void GetPercentOfPositiveReviews(Games game) {
            Console.WriteLine("Positive reviews: " + game.percentOfPositiveReviews + "%");
        }

    }
}
