using System;
namespace RaceTo21
{
	public enum Task // enum Task that will be used by Game.nextTask
	{
		GetNumberOfPlayers,
		GetNames,
		IntroducePlayers,
		AgreedScore, // added agreed score so players can choose a winning score for the whole game
		Intro,
		FirstTurn,
		PlayerTurn,
		CheckForEnd,
		GameOver,
		Final,
		Continue,
		Done // added Done to consider as the 'end of the game'
	}
}