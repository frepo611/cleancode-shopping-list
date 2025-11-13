using ShoppingList.Application.Interfaces;
using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using Xunit;

namespace ShoppingList.Tests;


public class ShoppingListServiceTests
{
    /// - GetAll_ShouldNotReturnMoreThanActualItemCount

    [Fact]
    public void GetAll_WhenEmpty_ShouldReturnEmptyList()
    {
        //Arrange
        var service = new ShoppingListService();

        //Act
        var actualShoppingList = service.GetAll();

        //Assert
        Assert.Empty(actualShoppingList);
    }
    [Theory]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(0)]
    public void GetAll_WithItems_ShouldReturnAllItemsCount(int expected)
    {
        // Arrange
        var service = new ShoppingListService();
        for (int i = 0; i < expected; i++)
        {
            service.Add("Banan", 1, "En fin banan");
        }
        
        //Act
        var actual = service.GetAll();
        var actualCount = actual.Count(item => item is not null);

        //Assert
        Assert.Equal(expected, actualCount);


    }

    [Fact]
    public void AddWithValidInputShouldReturnItem()
    {
        // Arrange
        var service = new ShoppingListService();
        var expectedItem = new ShoppingItem()
        {
            Name = "Banan",
            Quantity = 1,
            Notes = "en god frukt"
        };

        // Act
        var actual = service.Add(expectedItem.Name, expectedItem.Quantity, expectedItem.Notes);

        // Assert
        Assert.NotNull(expectedItem);
        Assert.Equal(expectedItem.Name, actual!.Name);
        Assert.Equal(expectedItem.Quantity, actual.Quantity);
    }

    [Fact]
    public void Add_ShouldGenerateUniqueId()
    {
        // Arrange
        var service = new ShoppingListService();
        
        //Act
        var item1 = service.Add("Banan", 1, "En fin banan");
        var item2 = service.Add("Banan", 1, "En fin banan");

        //Assert

        Assert.NotEqual(item1.Id, item2.Id);
    }



    private ShoppingItem[] AddTestData(IShoppingListService service)
    {
        var items = new ShoppingItem[5];
        service.Add("Banan", 1, "En fin banan");
        service.Add("Aples", 2, "En fin apple");
        service.Add("Kaffe", 3, "En fin kaffe");
        service.Add("Milk", 4, "En fin milk");
        service.Add("Kaka", 5, "En fin kaka");

        return items;
    }

}

/// Unit tests for ShoppingListService.

///
/// IMPORTANT: Write your tests here using Test Driven Development (TDD)
///
/// TDD Workflow:
/// 1. Write a test for a specific behavior (RED - test fails)
/// 2. Implement minimal code to make the test pass (GREEN - test passes)
/// 3. Refactor the code if needed (REFACTOR - improve without changing behavior)
/// 4. Repeat for the next behavior
///
/// Test Examples:
/// - See ShoppingItemTests.cs for examples of well-structured unit tests
/// - Follow the Arrange-Act-Assert pattern
/// - Use descriptive test names: Method_Scenario_ExpectedBehavior
///
/// What to Test:
/// - Happy path scenarios (normal, expected usage)
/// - Input validation (null/empty IDs, invalid parameters)
/// - Edge cases (empty array, array expansion, last item, etc.)
/// - Array management (shifting after delete, compacting, reordering)
/// - Search functionality (case-insensitive, matching in name/notes)
///
/// Recommended Test Categories:
///
/// GetAll() tests:
/// - GetAll_WhenEmpty_ShouldReturnEmptyList
/// - GetAll_WithItems_ShouldReturnAllItems
/// - GetAll_ShouldNotReturnMoreThanActualItemCount
///
/// GetById() tests:
/// - GetById_WithValidId_ShouldReturnItem
/// - GetById_WithInvalidId_ShouldReturnNull
/// - GetById_WithNullId_ShouldReturnNull
/// - GetById_WithEmptyId_ShouldReturnNull
///
/// Add() tests:
/// - Add_WithValidInput_ShouldReturnItem
/// - Add_ShouldGenerateUniqueId
/// - Add_ShouldIncrementItemCount
/// - Add_WhenArrayFull_ShouldExpandArray
/// - Add_AfterArrayExpansion_ShouldContinueWorking
/// - Add_ShouldSetIsPurchasedToFalse
///
/// Update() tests:
/// - Update_WithValidId_ShouldUpdateAndReturnItem
/// - Update_WithInvalidId_ShouldReturnNull
/// - Update_ShouldNotChangeId
/// - Update_ShouldNotChangeIsPurchased
///
/// Delete() tests:
/// - Delete_WithValidId_ShouldReturnTrue
/// - Delete_WithInvalidId_ShouldReturnFalse
/// - Delete_ShouldRemoveItemFromList
/// - Delete_ShouldShiftRemainingItems
/// - Delete_ShouldDecrementItemCount
/// - Delete_LastItem_ShouldWork
/// - Delete_FirstItem_ShouldWork
/// - Delete_MiddleItem_ShouldWork
///
/// Search() tests:
/// - Search_WithEmptyQuery_ShouldReturnAllItems
/// - Search_WithNullQuery_ShouldReturnAllItems
/// - Search_MatchingName_ShouldReturnItem
/// - Search_MatchingNotes_ShouldReturnItem
/// - Search_ShouldBeCaseInsensitive
/// - Search_WithNoMatches_ShouldReturnEmpty
/// - Search_ShouldFindPartialMatches
///
/// ClearPurchased() tests:
/// - ClearPurchased_WithNoPurchasedItems_ShouldReturnZero
/// - ClearPurchased_ShouldRemoveOnlyPurchasedItems
/// - ClearPurchased_ShouldReturnCorrectCount
/// - ClearPurchased_ShouldShiftRemainingItems
///
/// TogglePurchased() tests:
/// - TogglePurchased_WithValidId_ShouldReturnTrue
/// - TogglePurchased_WithInvalidId_ShouldReturnFalse
/// - TogglePurchased_ShouldToggleFromFalseToTrue
/// - TogglePurchased_ShouldToggleFromTrueToFalse
///
/// Reorder() tests:
/// - Reorder_WithValidOrder_ShouldReturnTrue
/// - Reorder_WithInvalidId_ShouldReturnFalse
/// - Reorder_WithMissingIds_ShouldReturnFalse
/// - Reorder_WithDuplicateIds_ShouldReturnFalse
/// - Reorder_ShouldChangeItemOrder
/// - Reorder_WithEmptyList_ShouldReturnFalse
/// </summary>
