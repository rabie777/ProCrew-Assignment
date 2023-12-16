 
using Google.Cloud.Firestore;
using FirebaseAdmin.Messaging;
using ProCrew_Assignment.Data;
using ProCrew_Assignment.Models;
using Firebase.Database;
using static Google.Rpc.Context.AttributeContext.Types;

namespace ProCrew_Assignment.Services
{
    public class AuditService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuditService> _logger;
        public AuditService(ApplicationDbContext context, ILogger<AuditService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task LogAuditAsync(string tableName, int recordId, string operation, string oldValues, string newValues)
        {
            var auditLog = new AuditLog
            {
                TableName = tableName,
                RecordId = recordId,
                Operation = operation,
                OldValues = oldValues,
                NewValues = newValues,
                Timestamp = DateTime.Now
            };

            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();


             
            //var database = FirebaseDatabase.DefaultInstance;

            //var reference = database.GetReference("auditLogs");
            //reference.Push().SetValueAsync(auditLog);

            var firebaseClient = new FirebaseClient("https://procrew-d052e-default-rtdb.firebaseio.com/");
            var auditData = new
            {
                TableName = auditLog.TableName,
                RecordId = auditLog.RecordId,
                Operation = auditLog.Operation,
                OldValues = auditLog.OldValues,
                NewValues = auditLog.NewValues,
                Timestamp = auditLog.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };
 

            PushToFirebase(auditLog);
        }
        private async void PushToFirebase(AuditLog auditLog)
        {
            // Customize the message as needed
            var message = new Message
            {
                Data = new Dictionary<string, string>
            {
                { "tableName", auditLog.TableName },
                { "recordId", auditLog.RecordId.ToString() },
                { "operation", auditLog.Operation },
                { "oldValues", auditLog.OldValues },
                { "newValues", auditLog.NewValues },
                { "timestamp", auditLog.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            },
                Topic = "Message"  
            };

       
           
            try
            {

                var res = await FirebaseMessaging.DefaultInstance.SendAsync(message);

               

                // Log success
                _logger.LogInformation("Firebase message sent successfully.");
            }
            catch (Exception ex)
            {
                // Log error if there's an exception
                _logger.LogError($"Error sending Firebase message: {ex.Message}");
            }
        }
    }
}
