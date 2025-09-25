using VendingLib;

public class ChangeTests
{   

    [Fact]
    public void GetChange_18_Return10_5_2_1()
    {
        // Arrange
        var vendingMachine = new VendingMachine();
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(5);
        vendingMachine.InsertCoin(2);
        vendingMachine.InsertCoin(1);

        // Act
        var change = vendingMachine.Cancel();

        // Assert
        Assert.Equal(1, change["10"]);
        Assert.Equal(1, change["5"]);
        Assert.Equal(1, change["2"]);
        Assert.Equal(1, change["1"]);
    }

    [Fact]
    public void GetChange_7_Return5_2()
    {
        // Arrange
        var vendingMachine = new VendingMachine();
        vendingMachine.InsertCoin(5);
        vendingMachine.InsertCoin(2);

        // Act
        var change = vendingMachine.Cancel();

        // Assert
        Assert.Equal(1, change["5"]);
        Assert.Equal(1, change["2"]);
        Assert.False(change.ContainsKey("10"));
        Assert.False(change.ContainsKey("1"));
    }

    [Fact]
    public void GetChange_0_ShouldReturnEmptyDictionary()
    {
        // Arrange
        var vendingMachine = new VendingMachine();

        // Act
        var change = vendingMachine.Cancel();

        // Assert
        Assert.Empty(change);
    }

    [Fact]
    public void GetChange_11_Return10_1()
    {
        // Arrange
        var vendingMachine = new VendingMachine();
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(1);

        // Act
        var change = vendingMachine.Cancel();

        // Assert
        Assert.Equal(1, change["10"]);
        Assert.Equal(1, change["1"]);
        Assert.False(change.ContainsKey("5"));
        Assert.False(change.ContainsKey("2"));
    }
}
