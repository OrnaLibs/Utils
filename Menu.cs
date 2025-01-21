namespace OrnaLibs
{
    public abstract class Menu
    {
        public abstract string Title { get; }
        public abstract string[] Names { get; }
        public abstract Action[] Actions { get; }

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
