using UnityEngine;

public interface ITerrainGenerator
{
    Color GetColor(float x, float y, float z);
}