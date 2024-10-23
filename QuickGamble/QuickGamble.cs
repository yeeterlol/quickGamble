using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace QuickGamble;

public class QuickGamble : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Minigames/ScratchTicket/scratch_ticket.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // you cant fucking understand this shit
        // why

        var spawnFishMatch = new MultiTokenWaiter([
            t => t.Type is TokenType.Dollar,
            t => t is IdentifierToken {Type: TokenType.Identifier, AssociatedData: 41, Name: "AnimationPlayer" },
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Type: TokenType.Identifier, AssociatedData: 42, Name: "play" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Type: TokenType.Constant, AssociatedData: 16, Value: StringVariant {Value: "intro" } },
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ]);

        var timeout = new MultiTokenWaiter([
            t => t.Type is TokenType.PrYield,
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is IdentifierToken {Type: TokenType.Identifier, AssociatedData: 38, Name: "get_tree" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Type: TokenType.Identifier, AssociatedData: 71, Name: "create_timer" },
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Type: TokenType.Constant, AssociatedData: 26, Value: RealVariant {Value: 0.4 } },
            t => t.Type is TokenType.ParenthesisClose,
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Type: TokenType.Constant, AssociatedData: 27, Value: StringVariant {Value: "timeout" } },
            t => t.Type is TokenType.ParenthesisClose,
        ]);

        foreach (var token in tokens) {
            if (spawnFishMatch.Check(token)) {
                yield return token;
                yield return new Token(TokenType.Newline, 1);

                // for scratch in get_tree().get_nodes_in_group("scratch_spot"):
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("scratch");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("get_nodes_in_group");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("scratch_spot"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);

                yield return new Token(TokenType.Newline, 2);

                //   scratch.emit_signal("_scratched")
                yield return new IdentifierToken("scratch");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("emit_signal");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("_scratched"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline);
            } else if (timeout.Check(token)) {
                yield return token;
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Newline, 1);
                yield return new Token(TokenType.Newline, 2);

                yield return new Token(TokenType.PrYield);
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("get_tree");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("create_timer");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new RealVariant(0.01));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("timeout"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);
                yield return new IdentifierToken("_end");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("won");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);
                yield return new Token(TokenType.Newline, 2);



            } else {
                yield return token;
            }

        }
    }
}

