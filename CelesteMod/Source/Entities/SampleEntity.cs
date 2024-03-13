using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.CelesteMod.Entities;

[CustomEntity("CelesteMod/SampleEntity")]
public class SampleEntity : Entity {
    public SampleEntity(EntityData data, Vector2 offset)
        : base(data.Position + offset) {
        // TODO: read properties from data
        Add(GFX.SpriteBank.Create("sampleEntity"));
        Collider = new Hitbox(16, 16, -8, -8);
    }
}