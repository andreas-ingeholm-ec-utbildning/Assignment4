static class ConsoleUtility
{

    public static int? Choose(params string[] items) =>
        Choose(null, items);

    public static int? Choose(Action? prompt, params string[] items)
    {

        for (int i = 0; i < items.Length; i++)
            Console.WriteLine(i + ". " + items[i]);

        prompt?.Invoke();

        return
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out var option)
            ? option
            : null;

    }

    public static void Choose(Action badInput, Action? prompt, params (string name, Action action)[] items)
    {

        var option = Choose(prompt, items.Select(i => i.name).ToArray());
        var action = items.ElementAtOrDefault(option ?? -1).action;
        (action ?? badInput).Invoke();

    }

    public static bool Prompt(Action prompt)
    {
        prompt.Invoke();
        return Choose("Yes", "No") == 0;
    }
}
