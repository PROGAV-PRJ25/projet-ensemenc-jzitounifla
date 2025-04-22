public static class Constantes
{
    public static readonly Dictionary<string, Dictionary<int, string>> Menus = new Dictionary<string, Dictionary<int, string>> {
    // Pièces 11+1*3+2, déphasage : + si grand, négatif si petit
    { "MenuGeneral", new Dictionary<int, string> {
        { 6, "Planter" },
        { 8, "Arroser" },
        { 14, "Retour"},
    }}
    };
}