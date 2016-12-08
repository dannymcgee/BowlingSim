using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class ActionMasterTest {

	private ActionMaster actionMaster;
	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

	[SetUp]
	public void SetUp() {
		actionMaster = new ActionMaster();
	}

	[Test]
	public void T00_PassingTest() {
		Assert.AreEqual( 1, 1 );
	}

	[Test]
	public void T01_OneStrikeReturnsEndTurn() {
		Assert.AreEqual( endTurn, actionMaster.Bowl( 10 ) );
	}

	[Test]
	public void T02_Bowl8ReturnsTidy() {
		Assert.AreEqual( tidy, actionMaster.Bowl( 8 ) );
	}

	[Test]
	public void T03_Bowl2plus8ReturnsEndTurn() {
		actionMaster.Bowl( 2 );
		Assert.AreEqual( endTurn, actionMaster.Bowl( 8 ) );
	}

	[Test]
	public void T04_CheckResetAtStrikeOnLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		Assert.AreEqual( reset, actionMaster.Bowl( 10 ) );
	}

	[Test]
	public void T05_CheckResetAtSpareOnLastFrame2ndBowl() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		actionMaster.Bowl( 1 );
		Assert.AreEqual( reset, actionMaster.Bowl( 9 ) );
	}

	[Test]
	public void T06_PlausiblePlaythroughTest1() {
		int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		Assert.AreEqual( endGame, actionMaster.Bowl( 9 ) );
	}

	[Test]
	public void T07_PlausiblePlaythroughTest2() {
		int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		Assert.AreEqual( endGame, actionMaster.Bowl( 1 ) );
	}

	[Test]
	public void T08_GameEndsAtBowl20() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		Assert.AreEqual( endGame, actionMaster.Bowl( 1 ) );
	}

	[Test]
	public void T09_CheckStrikeThen5OnLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		actionMaster.Bowl( 10 );
		Assert.AreEqual( tidy, actionMaster.Bowl( 5 ) );
	}

	[Test]
	public void T10_CheckStrikeThen0OnLastFrame() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		actionMaster.Bowl( 10 );
		Assert.AreEqual( tidy, actionMaster.Bowl( 0 ) );
	}

	[Test]
	public void T11_CheckSpareWith10onSecondRoll() {
		actionMaster.Bowl( 0 );
		actionMaster.Bowl( 10 );
		Assert.AreEqual( tidy, actionMaster.Bowl( 5 ) );
	}

	[Test]
	public void T12_UdemyTestCase() {
		int[] rolls = { 0, 10, 5 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		Assert.AreEqual( endTurn, actionMaster.Bowl( 1 ) );
	}

	[Test]
	public void T13_LastFrameTurkey() {
		int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
		foreach( int roll in rolls ) {
			actionMaster.Bowl( roll );
		}
		Assert.AreEqual( reset, actionMaster.Bowl( 10 ) );
		Assert.AreEqual( reset, actionMaster.Bowl( 10 ) );
		Assert.AreEqual( endGame, actionMaster.Bowl( 10 ) );
	}


}
