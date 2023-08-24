public class Define
{
    public enum BossState 
    { 
        pattern4_1,
        pattern4_2,
        pattern4_3,
        pattern4_4,
        pattern4_5
    }

    public enum CameraState
    {
        Player,
        None 
    }
    
    // 새로 만드는 씬들은 모두 여기에 업데이트해주기
    public enum Scene
    {
        MainScene,
        SnowWhiteMap,
        SnowBoss123,
        SnowBoss4,
        ClosetScene,
        StageScene
        ,None
    }

    public enum PlayerState
    {
        Jump,
        Damaged,
        Walk,
        FakeWalk,
        Idle,
        Fly
    }
    
    public enum Tags
    {
        Untagged,
        Enemy, 
        Player,
        Platform,
        PlayerDied,
        Flag,
        Item,
        Border,
        FallingBlock,
        Transparent
    }
    
    public enum RotateDelta
    {
        Left = 90,
        Right = -90,
        UpSideDown = 180
    }
}
