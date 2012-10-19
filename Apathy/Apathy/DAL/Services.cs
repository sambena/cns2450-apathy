namespace Apathy.DAL
{
    public static class Services
    {
        private static UnitOfWork _uow = new UnitOfWork();

        private static IEnvelopeService _envelopeService;
        private static ITransactionService _transactionService;
        private static IUserService _userService;

        public static IEnvelopeService EnvelopeService 
        {
            get
            {
                if (_envelopeService == null)
                    _envelopeService = new EnvelopeService(_uow);

                return _envelopeService;
            }
        }

        public static ITransactionService TransactionService 
        { 
            get
            {
                if (_transactionService == null)
                    _transactionService = new TransactionService(_uow);
                
                return _transactionService;
            }
        }

        public static IUserService UserService 
        { 
            get
            {
                if (_userService == null)
                    _userService = new UserService(_uow);

                return _userService;
            }
        }
    }
}