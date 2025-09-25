using VendingLib;

public class VendingMachineTests
{
    [Fact]
    public void BuyItem_WithoutMoney_ReturnsTrue()
    {
        // Arrange
        var vendingMachine = new VendingMachine();
        vendingMachine.AddProduct(new Product("Святой источник", 20, 1));
        vendingMachine.InsertCoin(10);
        vendingMachine.InsertCoin(10);

        // Act
        bool result = vendingMachine.BuyItem("Святой источник", out var product);

        // Assert
        Assert.True(result);
        Assert.Equal("Святой источник", product!.Name);
        Assert.Equal(0, product.Quantity);
    }

    [Fact]
    public void BuyItem_NotEnoughMoney_Fail()
    {
        // Arrange
        var vm = new VendingMachine();
        vm.AddProduct(new Product("Чипсы LavaLava", 30, 1));
        vm.InsertCoin(10);

        // Act
        bool result = vm.BuyItem("Чипсы LavaLava", out var product);

        // Assert
        Assert.False(result);
        Assert.Null(product);
    }
}