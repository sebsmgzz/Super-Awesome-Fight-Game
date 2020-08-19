using System.Collections.Generic;

public static partial class Game
{
    public static class AnimationsInfo
    {

        public static AnimationInfo Crouching =>
            new AnimationInfo(Animation.Crouching)
            {
                AnimatorName = "Crouching",
                Length = 0.2f,
                Loops = false
            };

        public static AnimationInfo Standing =>
            new AnimationInfo(Animation.Standing)
            {
                AnimatorName = "Standing",
                Length = 0.3f,
                Loops = false
            };

        public static AnimationInfo Covering =>
            new AnimationInfo(Animation.Covering)
            {
                AnimatorName = "Covering",
                Length = 0.333f,
                Loops = false
            };

        public static AnimationInfo Uncovering =>
            new AnimationInfo(Animation.Uncovering)
            {
                AnimatorName = "Uncovering",
                Length = 0.333f,
                Loops = false
            };

        public static AnimationInfo Falling =>
            new AnimationInfo(Animation.Falling)
            {
                AnimatorName = "Falling",
                Length = 0.5f,
                Loops = true
            };

        public static AnimationInfo Landing =>
            new AnimationInfo(Animation.Landing)
            {
                AnimatorName = "Landing",
                Length = 0.5f,
                Loops = false
            };

        public static AnimationInfo Launching =>
            new AnimationInfo(Animation.Launching)
            {
                AnimatorName = "Launching",
                Length = 0.5f,
                Loops = false
            };

        public static AnimationInfo Attacking =>
            new AnimationInfo(Animation.Attacking)
            {
                AnimatorName = "Attacking",
                Length = 0.667f,
                Loops = false
            };

        public static AnimationInfo Idling =>
            new AnimationInfo(Animation.Idling)
            {
                AnimatorName = "Idling",
                Length = 0.583f,
                Loops = true
            };

        public static AnimationInfo Running =>
            new AnimationInfo(Animation.Running)
            {
                AnimatorName = "Running",
                Length = 0.5f,
                Loops = true
            };

        public static AnimationInfo Throwing =>
            new AnimationInfo(Animation.Throwing)
            {
                AnimatorName = "Throwing",
                Length = 0.667f,
                Loops = false
            };

    }
}