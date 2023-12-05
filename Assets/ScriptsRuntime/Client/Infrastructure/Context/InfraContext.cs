using DC.Assets;
using DC.Template;
using DC.Database;
using DC.Events;

namespace DC.Infrastructure.Context {

    public class InfraContext {

        AssetsCore assetsCore;
        public AssetsCore AssetsCore => assetsCore;

        TemplateCore templateCore;
        public TemplateCore TemplateCore => templateCore;

        DatabaseCore dbCore;
        public DatabaseCore DBCore => dbCore;

        EventCore eventCore;
        public EventCore EventCore => eventCore;

        public InfraContext() {}

        public void Inject(AssetsCore assetsCore,
                           TemplateCore templateCore,
                           DatabaseCore dbCore,
                           EventCore eventCore) {
            this.assetsCore = assetsCore;
            this.templateCore = templateCore;
            this.dbCore = dbCore;
            this.eventCore = eventCore;
        }

    }

}