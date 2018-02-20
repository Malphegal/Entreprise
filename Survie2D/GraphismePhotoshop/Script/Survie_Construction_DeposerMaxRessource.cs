public static int SpendResource(int numberOfResourcesPossessed, int numberOfResourcesRequired)
{
    return numberOfResourcesPossessed >= numberOfResourcesRequired ? numberOfResourcesRequired : numberOfResourcesPossessed;
}