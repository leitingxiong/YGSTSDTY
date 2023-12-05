using UnityEngine;
using UnityEngine.UI;

namespace GreenRedNamespace
{

    // BattleApplication.Controller
    public class GreenRedMain : MonoBehaviour
    {

        // Context
        GreenRedContext context;

        // Domain
        GreenRedDomain domain;

        void Awake()
        {
            context = new GreenRedContext();
            domain = new GreenRedDomain();

            domain.Inject(context);

            // ==== Init ====
            domain.SpawnEntity();

        }

        void Update()
        {
            float dt = Time.deltaTime;
            domain.ExecuteFSM(dt);
        }

    }

}