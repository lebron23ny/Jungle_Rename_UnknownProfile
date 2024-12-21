using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM = Tekla.Structures.Model;
using TSC = Tekla.Structures.Catalogs;
using TSFC = Tekla.Structures.Filtering.Categories;
using TSF = Tekla.Structures.Filtering;
using Tekla.Structures.Filtering;

namespace Jungle_Rename_UnknownProfile.Core
{
    public static class Tools
    {
        public static TSM.ModelObjectEnumerator GetModelObjectEnumerator(string profFind)
        {
            TSM.Model model = new TSM.Model();

            TSFC.ObjectFilterExpressions.Type objectType = new TSFC.ObjectFilterExpressions.Type();
            TSF.NumericConstantFilterExpression type = new TSF.NumericConstantFilterExpression(Tekla.Structures.TeklaStructuresDatabaseTypeEnum.PART);
            TSF.BinaryFilterExpression AA = new TSF.BinaryFilterExpression(objectType, TSF.NumericOperatorType.IS_EQUAL, type);

            TSFC.PartFilterExpressions.Profile profileExpression = new TSFC.PartFilterExpressions.Profile();
            TSF.StringConstantFilterExpression partProfile = new StringConstantFilterExpression(profFind);
            TSF.BinaryFilterExpression A = new BinaryFilterExpression(profileExpression, StringOperatorType.IS_EQUAL, partProfile);

            TSF.BinaryFilterExpressionCollection expressionCollection = new BinaryFilterExpressionCollection();
            expressionCollection.Add(new BinaryFilterExpressionItem(AA, BinaryFilterOperatorType.BOOLEAN_AND));
            expressionCollection.Add(new BinaryFilterExpressionItem(A, BinaryFilterOperatorType.BOOLEAN_AND));

            return model.GetModelObjectSelector().GetObjectsByFilter(expressionCollection);
        }
    }
}
