public class Dialogue : MonoBehaviour
{
    public string npcName;
    public int numberOfDialogues;
    private string[] _dialogues;
    private int _currentDialogue;

    public void Awake()
    {
        npcName = GetComponentInParent<NPC>().name;
        _dialogues = new string[numberOfDialogues]();

        for (int i = 0; i < numberOfDialogues; i++)
            _dialogues[i] = Lang.GetString("npc.dialogue." + npcName + "." + i);

        _currentDialogue = 0;
    }

    public string GetNextDialogue()
    {
        return _dialogues[_currentDialogue++ %= numberOfDialogues]; // à tester
    }
}