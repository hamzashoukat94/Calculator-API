# Simple Calculator

```csharp
SimpleCalculatorController simpleCalculatorController = client.SimpleCalculatorController;
```

## Class Name

`SimpleCalculatorController`


# Calculate

Calculate the expression using specified operation

```csharp
CalculateAsync(
    Models.OperationTypeEnum operation,
    double x,
    double y)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `operation` | [`Models.OperationTypeEnum`](../../doc/models/operation-type-enum.md) | Template, Required | operation to perform |
| `x` | `double` | Query, Required | First value |
| `y` | `double` | Query, Required | second value |

## Response Type

`Task<double>`

## Example Usage

```csharp
OperationTypeEnum operation = OperationTypeEnum.MULTIPLY;
double x = 222.14;
double y = 165.14;

try
{
    double? result = await simpleCalculatorController.CalculateAsync(operation, x, y);
}
catch (ApiException e){};
```

