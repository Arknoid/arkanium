﻿﻿﻿namespace ScoreSpace.Interfaces
{
    public interface IDamageable
    {
        /// <summary>
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns>return true if isDie</returns>
        bool TakeDamage(int damageTaken = 5);
    }
}