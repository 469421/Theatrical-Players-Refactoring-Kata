namespace TheatricalPlayersRefactoringKata
{
    public class Performance
    {
        private int _audience;

        public string PlayID { get; set; }

        public int Audience { get => _audience; set => _audience = value; }

        public Performance(string playId, int audience)
        {
            this.PlayID = playId;
            this._audience = audience;
        }

    }
}
