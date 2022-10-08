using Core;
using UnityEngine;

namespace Fretson.Core
{
    [MainController]
    public class ApplicationController : MonoBehaviour
    {
        [AutoBind] public PrefabManager PrefabManager;

        private void Awake()
        {
            DIScope.Initialize(this);
        }
    }
}