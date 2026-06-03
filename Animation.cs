public class Animation
{
    private int startFrame;
    private int frameCount;
    private double frameTime;
    private bool loop;
    private double timer;
    private int currentFrame;

    public Animation(int startFrame, int frameCount, double frameTime, bool loop)
    {
        this.startFrame = startFrame;
        this.frameCount = frameCount;
        this.frameTime = frameTime;
        this.loop = loop;
    }

    public int CurrentFrame => currentFrame + startFrame;

    public void Update(double deltaTime)
    {
        timer += deltaTime;

        if(timer >= frameTime)
        {
            currentFrame++;
            timer = 0;
        }
        if(currentFrame > frameCount - 1)
        {
            if(loop) currentFrame = 0;
            else currentFrame = frameCount - 1;
        }
    }
    public bool IsEnded()
    {
        if(currentFrame == frameCount - 1 && !loop) return true;
        return false;
    }
    public void Reset()
    {
        currentFrame = 0;
        timer = 0;
    }
}