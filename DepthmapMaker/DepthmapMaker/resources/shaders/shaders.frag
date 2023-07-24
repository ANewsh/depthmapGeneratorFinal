#version 330

out vec4 outputColor;

uniform vec3 cameraPos;
uniform vec3 vertexPos;

void main()
{
    vec3 verDiff = vertexPos - cameraPos;
    vec3 verDiffSq = verDiff * verDiff;
    float distance = sqrt(verDiffSq[0]+verDiffSq[1]+verDiffSq[2]);

    //todo: find a way to map the distances on a scale of 0.0 - 1.0
    float val = 1.0 - (distance / 10.0);
    outputColor = vec4(val,0.2,0.2,1.0);
}