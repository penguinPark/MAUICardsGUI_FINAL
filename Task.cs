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
		GameOver,
		Final, // when the player reaches the total winning score
		Done // added Done to consider as the 'end of the game'
	}
}