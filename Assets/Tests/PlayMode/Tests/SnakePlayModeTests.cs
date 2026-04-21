using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SnakePlayModeTests
{
    private GameManager gameManager;
    private SnakeController snakeController;
    private FoodSpawner foodSpawner;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Cargar la escena principal antes de cada prueba
        SceneManager.LoadScene("MainScene");
        yield return null;
        yield return null;

        gameManager = Object.FindAnyObjectByType<GameManager>();
        snakeController = Object.FindAnyObjectByType<SnakeController>();
        foodSpawner = Object.FindAnyObjectByType<FoodSpawner>();

        Assert.IsNotNull(gameManager, "No se encontró GameManager en la escena.");
        Assert.IsNotNull(snakeController, "No se encontró SnakeController en la escena.");
        Assert.IsNotNull(foodSpawner, "No se encontró FoodSpawner en la escena.");
    }

    [UnityTest]
    public IEnumerator Snake_SeMueve_Automaticamente()
    {
        // =========================
        // ARRANGE
        // =========================
        Transform head = snakeController.transform.Find("SnakeHead");
        Assert.IsNotNull(head, "No se encontró SnakeHead como hijo del SnakeController.");

        Vector3 posicionInicial = head.position;

        // =========================
        // ACT
        // =========================
        yield return new WaitForSeconds(gameManager.MoveDelay + 0.05f);

        Vector3 posicionFinal = head.position;

        // =========================
        // ASSERT
        // =========================
        Assert.AreNotEqual(posicionInicial, posicionFinal,
            "La serpiente no cambió de posición después de un tick de movimiento.");

        Assert.Greater(posicionFinal.x, posicionInicial.x,
            "La serpiente debía comenzar moviéndose hacia la derecha.");
    }

    [UnityTest]
    public IEnumerator Snake_ComeComida_AumentaPuntaje_Y_Crece()
    {
        // =========================
        // ARRANGE
        // =========================
        int scoreInicial = GetPrivateField<int>(gameManager, "score");
        int segmentosIniciales = snakeController.transform.childCount;

        Vector2Int headCell = GetSnakeHeadCell();
        Vector2Int foodCell = headCell + Vector2Int.right;

        // Colocar la comida justo enfrente de la cabeza
        SetPrivateField(foodSpawner, "currentCell", foodCell);

        Transform foodView = foodSpawner.transform.Find("Food");
        Assert.IsNotNull(foodView, "No se encontró el objeto visual Food dentro de FoodSpawner.");
        foodView.position = gameManager.GridToWorld(foodCell);

        // =========================
        // ACT
        // =========================
        yield return new WaitForSeconds(gameManager.MoveDelay + 0.05f);

        // =========================
        // ASSERT
        // =========================
        int scoreFinal = GetPrivateField<int>(gameManager, "score");
        int segmentosFinales = snakeController.transform.childCount;

        Assert.AreEqual(scoreInicial + 1, scoreFinal,
            "El puntaje no aumentó en 1 después de comer la comida.");

        Assert.AreEqual(segmentosIniciales + 1, segmentosFinales,
            "La serpiente no creció en 1 segmento después de comer la comida.");
    }

    private Vector2Int GetSnakeHeadCell()
    {
        List<Vector2Int> cells = GetPrivateField<List<Vector2Int>>(snakeController, "cells");

        Assert.IsNotNull(cells, "No se pudo obtener la lista interna de celdas del snake.");
        Assert.IsTrue(cells.Count > 0, "La serpiente no tiene segmentos registrados.");

        return cells[0];
    }

    private static T GetPrivateField<T>(object instance, string fieldName)
    {
        FieldInfo field = instance.GetType().GetField(
            fieldName,
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        Assert.IsNotNull(field,
            $"No se encontró el campo privado '{fieldName}' en {instance.GetType().Name}.");

        return (T)field.GetValue(instance);
    }

    private static void SetPrivateField<T>(object instance, string fieldName, T value)
    {
        FieldInfo field = instance.GetType().GetField(
            fieldName,
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        Assert.IsNotNull(field,
            $"No se encontró el campo privado '{fieldName}' en {instance.GetType().Name}.");

        field.SetValue(instance, value);
    }
}