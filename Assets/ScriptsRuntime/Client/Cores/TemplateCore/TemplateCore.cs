using System.Threading.Tasks;

namespace DC.Template {

    public class TemplateCore {

        ChestTemplate chestTemplate;
        public ChestTemplate ChestTemplate => chestTemplate;

        MinionTemplate minionTemplate;
        public MinionTemplate MinionTemplate => minionTemplate;

        MissionTemplate missionTemplate;
        public MissionTemplate MissionTemplate => missionTemplate;

        public TemplateCore() {
            chestTemplate = new ChestTemplate();
            minionTemplate = new MinionTemplate();
            missionTemplate = new MissionTemplate();
        }

        public async Task LoadAll() {
            await chestTemplate.LoadAll();
            await minionTemplate.LoadAll();
            await missionTemplate.LoadAll();
        }

    }

}