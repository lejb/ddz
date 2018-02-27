using System.Collections.Generic;
using Logic.Core;
using Logic.DDZ.Types;

namespace Logic.DDZ
{
    public class DDZClassifier : Classifier
    {
        private static List<TypeCreator> typeCreators = new List<TypeCreator>()
        {
            DDZ_1.Creator, 
            DDZ_2.Creator,
            DDZ_3.Creator,
            DDZ_Bomb.Creator,
            DDZ_SuperBomb.Creator,
            DDZ_3_1.Creator,
            DDZ_3_2.Creator,
            DDZ_4_2.Creator,
            DDZ_4_2_2.Creator,
            DDZ_Straight.Creator,
            DDZ_Straight2.Creator,
            DDZ_Plane0.Creator,
            DDZ_Plane1.Creator,
            DDZ_Plane2.Creator
        };

        protected override IEnumerable<TypeCreator> GetTypeCreators()
        {
            return typeCreators;
        }
    }
}
