using ScoreSpace.Interfaces;
using UnityEngine;

namespace ScoreSpace.Core
{
    public class ScorePoint : MonoBehaviour, IScorePoints
    {

        [SerializeField] private int _points;
       public int PointsToAdd
       {
           get => _points;
           set => _points = value;
       }
       
    }
}