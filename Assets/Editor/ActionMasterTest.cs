using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

[TestFixture]
public class ActionMasterTest {

	private List<int> pinFalls;
	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

	[SetUp]
	public void SetUp() {
		pinFalls = new List<int>();
	}

	[Test]
	public void T00_PassingTest() {
		Assert.AreEqual( 1, 1 );
	}

	[Test]
	public void T01_OneStrikeReturnsEndTurn() {
		pinFalls.Add( 10 );
		Assert.AreEqual( endTurn, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T02_Bowl8ReturnsTidy() {
		pinFalls.Add( 8 );
		Assert.AreEqual( tidy, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T03_Bowl2plus8ReturnsEndTurn() {
		
		int[] rolls = { 2, 8 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}

		Assert.AreEqual( endTurn, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T04_CheckResetAtStrikeOnLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( reset, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T05_CheckResetAtSpareOnLastFrame2ndBowl() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( reset, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T06_PlausiblePlaythroughTest1() {
		int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 9 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( endGame, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T07_PlausiblePlaythroughTest2() {
		int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 1 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( endGame, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T08_GameEndsAtBowl20() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( endGame, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T09_CheckStrikeThen5OnLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 5 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( tidy, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T10_CheckStrikeThen0OnLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( tidy, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T11_CheckSpareWith10onSecondRoll() {
		int[] rolls = { 0, 10, 5 };
		Assert.AreEqual( tidy, ActionMaster.NextAction( rolls.ToList() ) );
	}

	[Test]
	public void T12_UdemyTestCase() {
		int[] rolls = { 0, 10, 5, 1 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( endTurn, ActionMaster.NextAction( pinFalls ) );
	}

	[Test]
	public void T13_LastFrameTurkey() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
		foreach( int roll in rolls ) {
			pinFalls.Add( roll );
		}
		Assert.AreEqual( reset, ActionMaster.NextAction( pinFalls ) );
		pinFalls.Add( 10 );
		Assert.AreEqual( reset, ActionMaster.NextAction( pinFalls ) );
		pinFalls.Add( 10 );
		Assert.AreEqual( endGame, ActionMaster.NextAction( pinFalls ) );
	}


}
