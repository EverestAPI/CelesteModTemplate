module CelesteModSampleTrigger

using ..Ahorn, Maple

@mapdef Trigger "CelesteMod/SampleTrigger" SampleTrigger(
    x::Integer, y::Integer, width::Integer=Maple.defaultTriggerWidth, height::Integer=Maple.defaultTriggerHeight,
    sampleProperty::Integer=0
)

const placements = Ahorn.PlacementDict(
    "Sample Trigger (CelesteMod)" => Ahorn.EntityPlacement(
        SampleTrigger,
        "rectangle",
    )
)

end