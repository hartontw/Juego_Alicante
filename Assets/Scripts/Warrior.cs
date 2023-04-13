using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    PascalCase
    camelCase
    snake_case
    kebab-case
*/
/*
C#
    Cabeceras y propiedades: PascalCase
    Atributos y variables: camelCase
*/

public class Warrior : Soldier
{
    protected override void Attack(Actor target)
    {
        base.Attack(target);
        target.ReceiveDamage(hitDamage);
    }
}
