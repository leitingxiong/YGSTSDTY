namespace ScriptsRuntime.Client.Controllers.Battle.Model {

    public class CalculationResultModel {

        public int whoWin; // 0: 没出结果; 1: minion; 2: monster; 3: 平局
        public int totalTurn; // 总回合 (来行动 + 回行动)
        public CalculationTurnModel[] turnArray; // 所有回合

        public CalculationResultModel() { }

        public CalculationActModel GetCurrentAct(int turnIndex, int actIndex) {
            if (turnArray == null || turnIndex >= totalTurn) {
                return null;
            }

            var turnModel = turnArray[turnIndex];
            var actArray = turnModel.actArray;
            if (actArray == null || actIndex >= actArray.Length) {
                return null;
            }

            return actArray[actIndex];

        }

        public bool IsOverAct(int turnIndex, int actIndex) {
            if (turnArray == null || turnIndex >= totalTurn) {
                return true;
            }

            var turnModel = turnArray[turnIndex];
            var actArray = turnModel.actArray;
            if (actArray == null || actIndex >= actArray.Length) {
                return true;
            }

            return false;
        }

        public bool IsOverTurn(int turnIndex) {
            if (turnArray == null || turnIndex >= totalTurn) {
                return true;
            }

            var turnModel = turnArray[turnIndex];
            var actArray = turnModel.actArray;
            if (actArray == null || actArray.Length == 0) {
                return true;
            }

            return false;
        }

    }

}