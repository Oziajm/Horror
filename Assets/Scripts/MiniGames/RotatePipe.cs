using UnityEngine;

public class RotatePipe : Interactable
{
    public Plumber.Tile Tile { get; set; }

    public override void Interact()
    {
        RotatePipes();
    }
    public override string GetHoverText()
    {
        return "Rotate";
    }

    private void RotatePipes()
    {
        transform.Rotate(0, 0, 90);
        Tile.entrances[0] = (Plumber.TileEntrance)(((int)Tile.entrances[0] + 1) % 4);
        Tile.entrances[1] = (Plumber.TileEntrance)(((int)Tile.entrances[1] + 1) % 4);
    }
}