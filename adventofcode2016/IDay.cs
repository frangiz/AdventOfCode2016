namespace adventofcode2016
{
	interface IDay
	{
		string Name { get; }

		string GetAnswerA(bool animate = false);
		string GetAnswerB(bool animate = false);
	}
}
