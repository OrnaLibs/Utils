namespace OrnaLibs
{
    public abstract class Menu
    {
        public abstract string Title { get; protected set; }
        public abstract string[] Names { get; protected set; }
        public abstract Action[] Actions { get; protected set; }

        public void Open()
        {
            int index;
            do
            {
                index = ConsoleUI.Select(Title, Names);
                if (index < Actions.Length)
                    Actions[index]();
            } while (index < Actions.Length);
        }
    }
}
