# Snake Setup en Unity

## Escena principal

1. Abre `Assets/Scenes/MainScene.unity`.
2. Elimina objetos que no necesites y deja la escena limpia.
3. En la cámara principal usa:
   - `Projection`: Orthographic
   - `Position`: `(0, 0, -10)`
   - `Size`: `11`
4. Crea tres objetos vacíos:
   - `GameManager`
   - `Snake`
   - `FoodSpawner`

## Scripts

1. Arrastra `GameManager.cs` al objeto `GameManager`.
2. Arrastra `SnakeController.cs` al objeto `Snake`.
3. Arrastra `FoodSpawner.cs` al objeto `FoodSpawner`.
4. En el inspector de `GameManager`, asigna:
   - `Snake Controller` -> objeto `Snake`
   - `Food Spawner` -> objeto `FoodSpawner`

## Cómo jugar

- Ejecuta la escena.
- Mueve con `WASD` o flechas.
- Reinicia con `R`.

## Notas

- Todo se dibuja por código, así que no necesitas sprites ni prefabs.
- El movimiento es por grid y ocurre en una sola escena.
