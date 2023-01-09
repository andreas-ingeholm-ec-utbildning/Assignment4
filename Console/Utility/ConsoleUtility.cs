static class ConsoleUtility
{

    /// <summary>Displays a numbered list of options to the user and prompts to select one.</summary>
    /// <param name="afterList">A action that is invoked when options has been listed, before input is checked.</param>
    /// <returns><see langword="int"/> index of the selected option. <see langword="null"/> if invalid option was chosen.</returns>
    public static int? Choose(Action? afterList, params string[] options)
    {

        for (int i = 0; i < options.Length; i++)
            Console.WriteLine(i + ". " + options[i]);

        afterList?.Invoke();

        return
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out var option)
            ? option
            : null;

    }

    /// <inheritdoc cref="Choose(Action?, string[])"/>
    public static int? Choose(params string[] options) =>
        Choose(null, options);

    /// <summary>Displays a numbered list of options to the user and prompts to select one. The action associated with the option will be invoked.</summary>
    /// <param name="prompt">Prompt to display after listing options.</param>
    /// <param name="badInput">Action to invoke when bad input was recieved.</param>
    public static void Choose(Action badInput, Action? prompt, params (string name, Action action)[] options)
    {

        var option = Choose(prompt, options.Select(i => i.name).ToArray());
        var action = options.ElementAtOrDefault(option ?? -1).action;
        (action ?? badInput).Invoke();

    }

    /// <summary>Displays a yes / no prompt and returns <see langword="true"/> or <see langword="false"/> depending on answer.</summary>
    /// <param name="prompt">The prompt to display to the user.</param>
    public static bool Prompt(string prompt)
    {
        Console.WriteLine(prompt);
        return Choose("Yes", "No") == 0;
    }

}
