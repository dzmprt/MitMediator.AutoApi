namespace MitMediator.AutoApi.Abstractions;

internal static class Pluralizer
{
    internal static string Pluralize(string singular)
    {
        if (string.IsNullOrEmpty(singular))
            return singular;
        
        if (IsPlural(singular))
            return singular;
        
        var lower = singular.ToLowerInvariant();
                
        switch (lower)
        {
            case "man": return ReplaceCase(singular, "men");
            case "woman": return ReplaceCase(singular, "women");
            case "child": return ReplaceCase(singular, "children");
            case "tooth": return ReplaceCase(singular, "teeth");
            case "foot": return ReplaceCase(singular, "feet");
            case "mouse": return ReplaceCase(singular, "mice");
            case "goose": return ReplaceCase(singular, "geese");
            case "person": return ReplaceCase(singular, "people");
            case "ox": return ReplaceCase(singular, "oxen");
            case "louse": return ReplaceCase(singular, "lice");
            case "cactus": return ReplaceCase(singular, "cacti");
            case "focus": return ReplaceCase(singular, "foci");
            case "fungus": return ReplaceCase(singular, "fungi");
            case "nucleus": return ReplaceCase(singular, "nuclei");
            case "syllabus": return ReplaceCase(singular, "syllabi");
            case "analysis": return ReplaceCase(singular, "analyses");
            case "diagnosis": return ReplaceCase(singular, "diagnoses");
            case "ellipsis": return ReplaceCase(singular, "ellipses");
            case "index": return ReplaceCase(singular, "indices");
            case "appendix": return ReplaceCase(singular, "appendices");
            case "criterion": return ReplaceCase(singular, "criteria");
            case "phenomenon": return ReplaceCase(singular, "phenomena");
            case "datum": return ReplaceCase(singular, "data");
            case "medium": return ReplaceCase(singular, "media");
            case "bacterium": return ReplaceCase(singular, "bacteria");
            case "curriculum": return ReplaceCase(singular, "curricula");
            case "formula": return ReplaceCase(singular, "formulas");
            case "thesis": return ReplaceCase(singular, "theses");
            case "basis": return ReplaceCase(singular, "bases");
            case "parenthesis": return ReplaceCase(singular, "parentheses");
            case "quiz": return ReplaceCase(singular, "quizzes");
            case "wife": return ReplaceCase(singular, "wives");
            case "life": return ReplaceCase(singular, "lives");
            case "leaf": return ReplaceCase(singular, "leaves");
            case "loaf": return ReplaceCase(singular, "loaves");
            case "calf": return ReplaceCase(singular, "calves");
            case "elf": return ReplaceCase(singular, "elves");
            case "shelf": return ReplaceCase(singular, "shelves");
            case "thief": return ReplaceCase(singular, "thieves");
            case "wolf": return ReplaceCase(singular, "wolves");
            case "scarf": return ReplaceCase(singular, "scarves");
            case "hero": return ReplaceCase(singular, "heroes");
            case "potato": return ReplaceCase(singular, "potatoes");
            case "tomato": return ReplaceCase(singular, "tomatoes");
            case "echo": return ReplaceCase(singular, "echoes");
            case "veto": return ReplaceCase(singular, "vetoes");
            case "cargo": return ReplaceCase(singular, "cargoes");
            case "mosquito": return ReplaceCase(singular, "mosquitoes");
            case "tornado": return ReplaceCase(singular, "tornadoes");
            case "volcano": return ReplaceCase(singular, "volcanoes");
            case "embargo": return ReplaceCase(singular, "embargoes");
            case "deer":
            case "sheep":
            case "fish":
            case "series":
            case "single":
            case "species":
            case "moose":
            case "aircraft":
                return singular;
        }

        ReadOnlySpan<char> word = singular;
        
        if (word.Length > 1 &&
            word[^1] == 'y' &&
            !"aeiou".Contains(word[^2]))
        {
            return singular.Substring(0, word.Length - 1) + "ies";
        }
        
        if (word.EndsWith("s", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("x", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("z", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("ch", StringComparison.OrdinalIgnoreCase) ||
            word.EndsWith("sh", StringComparison.OrdinalIgnoreCase))
        {
            return singular + "es";
        }

        return singular + "s";
    }
    
    internal static bool IsPlural(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return false;

        var lower = word.ToLowerInvariant();
        
        switch (lower)
        {
            case "deer": case "sheep": case "fish":
            case "series": case "species": case "moose": case "aircraft":
                return true;
        }
        
        if (lower.EndsWith("s") &&
            !lower.EndsWith("ss") &&
            !lower.EndsWith("us") &&
            !lower.EndsWith("is"))
            return true;

        return false;
    }
    
    private static string ReplaceCase(string original, string plural)
    {
        if (char.IsUpper(original[0]))
            return char.ToUpper(plural[0]) + plural[1..];
        return plural;
    }
}
